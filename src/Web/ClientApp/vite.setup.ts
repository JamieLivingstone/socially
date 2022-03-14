import moxios from 'moxios';
import { setLogger } from 'react-query';

setLogger({
  log: console.log,
  warn: console.warn,
  error: () => {
    // Do not log network errors in tests
  },
});

beforeEach(() => {
  moxios.install();
});

afterEach(() => {
  moxios.uninstall();
});
