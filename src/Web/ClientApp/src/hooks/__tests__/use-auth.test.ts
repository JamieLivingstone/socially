import { act, renderHook } from '@testing-library/react-hooks';
import axios from 'axios';
import moxios from 'moxios';

import Providers from '@providers';

import { useAuth } from '../use-auth';

describe('useAuth', () => {
  beforeEach(() => {
    moxios.stubRequest('/api/v1/accounts/current', {
      status: 200,
      response: {
        name: 'John Doe',
        username: 'john',
        email: 'john@test.com',
      },
    });
  });

  afterEach(() => {
    document.cookie = 'token=';
  });

  describe('new session', () => {
    test('account is null', async () => {
      const { result } = renderHook(() => useAuth(), { wrapper: Providers });

      expect(result.current.account).toBeNull();
    });
  });

  describe('existing session', () => {
    test('fetches account', async () => {
      document.cookie = 'token=mock-token;';

      const { result, waitForNextUpdate } = renderHook(() => useAuth(), { wrapper: Providers });

      await waitForNextUpdate();

      expect(axios.defaults.headers.common.Authorization).toEqual('Bearer mock-token');
      expect(result.current.account).toMatchSnapshot();
    });
  });

  describe('login', () => {
    describe('given valid credentials', () => {
      test('sets auth token and fetches current account', async () => {
        moxios.stubRequest('/api/v1/accounts/login', {
          status: 200,
          response: { token: 'mock-token' },
        });

        const { result } = renderHook(() => useAuth(), { wrapper: Providers });

        expect(result.current.account).toBeNull();

        await act(() => result.current.login({ username: 'user', password: 'password' }));

        expect(result.current.account).not.toBeNull();
      });
    });
  });

  describe('register', () => {
    describe('given a valid payload', () => {
      test('sets auth token and fetches current account', async () => {
        moxios.stubRequest('/api/v1/accounts/register', {
          status: 201,
          response: { token: 'mock-token' },
        });

        const { result } = renderHook(() => useAuth(), { wrapper: Providers });

        expect(result.current.account).toBeNull();

        await act(() =>
          result.current.register({
            email: 'john@test.com',
            name: 'John Doe',
            password: 'password',
            username: 'john',
          }),
        );

        expect(result.current.account).not.toBeNull();
      });
    });
  });

  describe('logout', () => {
    test('removes cookie and sets account to null', async () => {
      document.cookie = 'token=mock-auth-token;';

      const { result, waitForNextUpdate } = renderHook(() => useAuth(), { wrapper: Providers });

      await waitForNextUpdate();

      expect(result.current.account).not.toBeNull();

      act(() => result.current.logout());

      expect(document.cookie).toEqual('');
      expect(result.current.account).toBeNull();
    });
  });
});
