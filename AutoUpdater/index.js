const path = require('path');
const { createWriteStream } = require('fs');
const streamer = require('./lib/streamer');
const fsutils = require('./lib/fsutils');
const swfutils = require('./lib/swfutils');

const ASSET_ENDPOINT = 'https://static.drips.pw/rotmg/production/current/';
const PACKET_REGEX = /public static const ([A-Z_]+):int = (\d+);/g;

const clientPath = path.join(__dirname, '..', 'Resources', 'client.swf');
const objectsPath = path.join(__dirname, '..', 'Resources', 'Objects.xml');
const tilesPath = path.join(__dirname, '..', 'Resources', 'Tiles.xml');
const packetsPath = path.join(__dirname, '..', 'Resources', 'Packets.xml');
const rawPath = path.join(
    __dirname, '..', 'Resources', 'scripts',
    'kabam', 'rotmg', 'messaging',
    'impl', 'GameServerConnection.as'
);

fsutils.make(path.join(__dirname, '..', 'Resources'));
fsutils.empty(clientPath);
fsutils.empty(objectsPath);
fsutils.empty(tilesPath);
fsutils.empty(packetsPath);

const clientStream = createWriteStream(clientPath);
const objectStream = createWriteStream(objectsPath);
const tileStream = createWriteStream(tilesPath);

async function run() {
    console.log('(1/3) Downloading assets.');
    try {
        await Promise.all([
            streamer.streamInto(`${ASSET_ENDPOINT}/xmlc/Objects.xml`, objectStream, 'Objects.xml'),
            streamer.streamInto(`${ASSET_ENDPOINT}/xmlc/GroundTypes.xml`, tileStream, 'Tiles.xml'),
            streamer.streamInto(`${ASSET_ENDPOINT}/client.swf`, clientStream, 'client.swf')
        ]);
        console.log('(2/3) Decompiling swf.');
        await swfutils.unpackInto(path.join(__dirname, 'lib', 'jpexs', 'ffdec.jar'), path.join(__dirname, '..', 'Resources'));
        console.log('(3/3) Creating Packets.xml');
        const raw = fsutils.read(rawPath);
        const packetStream = createWriteStream(packetsPath);
        packetStream.write('<Packets>\n');
        let match = PACKET_REGEX.exec(raw);
        while (match != null) {
            packetStream.write(`\t<Packet>\n\t\t<PacketName>${match[1].replace(/_/, '')}</PacketName>\n\t\t<PacketID>${match[2]}</PacketID>\n\t</Packet>\n`);
            match = PACKET_REGEX.exec(raw);
        }
        packetStream.write('</Packets>\n');
        packetStream.end();
        console.log('\nDone!');
    } catch (error) {
        console.log('Error while updating.');
        console.log(error.message);
    }
}
run();
