// Add into main window preload somewhere :)
setTimeout(async () => document.styleSheets[0].insertRule(`.socialLinks-3ywLUf + .info-3pQQBb::after {
  content: 'Electron ${process.versions.electron} | ${process.arch === 'x64' ? '64' : '32'} bit \\a Chromium ${process.versions.chrome} \\a Node ${process.versions.node}';
  white-space: pre-wrap;
  text-transform: none;
  color: var(--text-muted);
  font-weight: 400;
  font-family: var(--font-primary);
  font-size: 12px;
  line-height: 16px;
  width: 100%;
  padding: 8px 0;
  margin: 8px 0;
  display: inline-block;
  border-top: 1px solid var(--background-modifier-accent);
}`), 2000);
