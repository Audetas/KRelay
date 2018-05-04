const { exec } = require('child_process');
const path = require('path');

/**
 * Unpacks client.swf
 * 
 * **client.swf must be in the given directory.**
 * @param {string} dir The directory to unpack the swf into.
 */
function unpackInto(dir) {
    return new Promise((resolve, reject) => {
        const args = [
            '-jar',
            `${path.join(__dirname, 'jpexs', 'ffdec.jar')}`,
            '-selectclass kabam.rotmg.messaging.impl.GameServerConnection',
            '-export script',
            `${dir}`,
            `${path.join(dir, 'client.swf')}`
        ];
        exec(`java ${args.join(' ')}`, (error, stdout, stderr) => {
            if (error) {
                reject(error);
                return;
            }
            resolve();
        });
    });
}

exports.unpackInto = unpackInto;