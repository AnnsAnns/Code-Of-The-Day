const last = {
  canary: {},
  ptb: {},
  stable: {},
  development: {}
};

const check = async (channel) => {
  const manifest = await (await fetch(`https://discord.com/api/updates/distributions/app/manifests/latest?platform=win&channel=${channel}&arch=x86`)).json();

  for (const m in manifest.modules) {
    const mod = manifest.modules[m];
    const ver = mod.full.module_version;

    if (last[channel][m]) {
      if (last[channel][m] !== ver) console.log('UPDATED!', channel, m, last[channel][m], '->', ver);
    }

    last[channel][m] = ver;
  }
};

const yes = async () => {
  process.stdout.write('checking...');

  await check('canary');
  await check('ptb');
  await check('stable');
  await check('development');

  console.log('\rchecked.           ');
};

yes();
yes();
setInterval(yes, 1000 * 60 * 4);
