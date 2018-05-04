const fs = require('fs');

/**
 * Ensures that the `file` exists and is empty.
 * @param {fs.PathLike} file The path to the file.
 */
function empty(file) {
    try {
        fs.truncateSync(file, 0);
    } catch (error) {
        fs.writeFileSync(file, '', { flag: 'w' });
    }
}

/**
 * Reads the file as a UTF8 string.
 * @param {string} file The file to read.
 */
function read(file) {
    return fs.readFileSync(file, { encoding: 'utf8' });
}

/**
 * Creates a directory. Ignores `EEXIST` but will rethrow anything else.
 * @param {string} dir The directory to make.
 */
function make(dir) {
    try {
        fs.mkdirSync(dir);
    } catch(error) {
        if (error.code !== 'EEXIST') {
            throw error;
        }
    }
}

exports.empty = empty;
exports.read = read;
exports.make = make;