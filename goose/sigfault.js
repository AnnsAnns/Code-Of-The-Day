import { join } from 'path';
import { openSync, writeSync, closeSync } from 'fs';

// credit to https://github.com/xirreal/krispPatch/blob/main/updater.js
const patch = (filename, offset, bytes) => {
  const fd = openSync(filename, "r+");
  writeSync(fd, bytes, 0, bytes.length, offset);
  closeSync(fd);
};

export default dir => {
  // patch(join(dir, 'discord_krisp.node'), 0xAF5C, new Uint8Array([0x90, 0x90, 0x90, 0x90, 0x90, 0x90])); // old
  patch(join(dir, 'discord_krisp.node'), 0xABCD, new Uint8Array([0x00])); // new (~31st Aug)
  log('patched krisp sigcheck');
};
