const { WriteStream } = require('fs');
const https = require('https');

/**
 * Streams content from the endpoint into the `writeStream`.
 * @param {string} endpoint The endpoint to stream from.
 * @param {WriteStream} writeStream The `WriteStream` to stream into.
 */
function streamInto(endpoint, writeStream, streamName) {
    return new Promise((resolve, reject) => {
        console.log(`Downloading ${streamName}...`);
        https.get(endpoint, (res) => {
            res.once('error', reject);
            res.on('data', (chunk) => writeStream.write(chunk));
            res.once('end', () => {
                console.log(`Finished ${streamName}`);
                res.removeAllListeners('data');
                writeStream.end();
                resolve();
            })
        });
    })
}

exports.streamInto = streamInto;
