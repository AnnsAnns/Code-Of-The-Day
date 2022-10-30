(async () => {
const { join } = require('path');
const { get } = require('https');
const zlib = require('zlib');
const cp = require('child_process');
const fs = require('fs');

let [ channel, mod, version ] = process.argv.slice(2);
channel = channel ?? 'canary';

console.log(channel, mod, version);

const manifest = await (await fetch(`https://discord.com/api/updates/distributions/app/manifests/latest?platform=win&channel=${channel}&arch=x86`)).json();

version = version ?? (mod === 'host' ? manifest.full.host_version[2] : manifest.modules['discord_' + mod].full.module_version);

const domain = `https://dl${channel === 'stable' ? '' : `-${channel}`}.discordapp.net`;

// const downloadUrl = (mod === 'host' ? manifest : manifest.modules['discord_' + mod]).full.url;
const downloadUrl = mod === 'host' ? `${domain}/distro/app/${channel}/win/x86/1.0.${version}/full.distro` : `${domain}/distro/app/${channel}/win/x86/1.0.${manifest.full.host_version[2]}/discord_${mod}/${version}/full.distro`;
console.log('DOWNLOADING', mod, version, '|', downloadUrl);

const path = `${mod}-${version}`;

const tarPath = path + '.tar';
const finalPath = path;

fs.rmSync(tarPath, { force: true });
fs.rmSync(finalPath, { recursive: true, force: true });

// await fs.promises.mkdir(dirname(tarPath)).catch(_ => {});

const stream = zlib.createBrotliDecompress();
stream.pipe(fs.createWriteStream(tarPath));

let downloadTotal = 0, downloadCurrent = 0;
get(downloadUrl, res => { // query for caching
  res.pipe(stream);

  downloadTotal = parseInt(res.headers['content-length'] ?? 1, 10);

  res.on('data', c => {
    downloadCurrent += c.length;

    console.log((downloadCurrent / downloadTotal) * 100);
  });
});

await new Promise(res => stream.on('end', res));

await fs.promises.mkdir(finalPath, { recursive: true }).catch(_ => {});

const proc = cp.execFile('tar', [ '--strip-components', '1', '-xf', tarPath, '-C', finalPath]);
await new Promise(res => proc.on('close', res));

console.log('DOWNLOADED', finalPath);

const asar = require('asar');

for (const f of fs.readdirSync(finalPath)) {
  const p = join(finalPath, f);
  if (f.endsWith('.asar')) {
    console.log('extracting', f);

    asar.extractAll(p, p.replace('.asar', ''));
  }
}
})();
