const loopSigma = (r, n, expression) => {
  const evalExp = expression.replaceAll('^', '**').replaceAll(/[0-9]+r/g, (_) => _.replace('r', '*r'));

  let out = 0;
  for (; r <= n; r++) {
    const next = eval(evalExp);
    out += next;
  }

  return out;
};

const precisionPoint = 196600;

const optSigma = (r, n, expression) => {
  r -= 1;

  if (r === 0) return _optSigma(n, expression);

  return Math.abs(_optSigma(n, expression) - _optSigma(r, expression));
};

const _optSigma = (n, expression) => { // optimised sigma
  const apply = (base, operation, value) => {
    switch (operation) {
      case '+': {
        return base + value;
      }

      case '-': {
        return base - value;
      }

      case '/': {
        return base / value;
      }

      case '*': {
        return base * value;
      }
    }
  };

  expression = expression + ' ';

  let out = 0;

  const exParsed = expression.split(/(?=[+\-*/])/);

  for (const term of exParsed) {
    const spaceSplit = term.split(' ');
    const [
      operator,
      subExp
    ] = spaceSplit.length === 2 ? [ '+', spaceSplit[0] ] : spaceSplit;

    let [
      amount,
      variable
    ] = subExp.split(/(?=[r])/);
    if (!variable && amount && subExp.includes('r')) {
      variable = amount;
      amount = '1';
    }

    amount = parseFloat(amount);

    // console.log(subExp, amount, variable);

    switch (variable) {
      case undefined: {
        out = apply(out, operator, amount * n);
        break;
      }

      case 'r': {
        out = apply(out, operator, amount * 0.5 * n * (n + 1));
        break;
      }

      case 'r^2': {
        out = apply(out, operator, amount * (1 / 6) * n * (n + 1) * (n * 2 + 1));
        break;
      }
    }
  }

  return out;
};

const args = [ 10, 1000, '2r^2 + 4r - 1' ]

console.time('opt');
const opt = Math.round(optSigma(...args));
console.timeEnd('opt');

console.time('loop');
const loop = loopSigma(...args);
console.timeEnd('loop');

console.log({ opt, loop, diff: loop - opt });

/* for (let i = 196600; i < 1000000; i += 10000) {// for (; rMulti <= 0.55; rMulti += 0.01) {
  args[1] = i;

  const opt = optSigma(...args);

  const loop = loopSigma(...args);

  const diff = Math.round(opt) - Math.round(loop);
  console.log(i, diff);
} */
