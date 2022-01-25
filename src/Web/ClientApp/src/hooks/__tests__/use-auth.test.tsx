import { act, renderHook } from '@testing-library/react-hooks';
import axios from 'axios';
import * as moxios from 'moxios';

import { useAuth } from '../use-auth';
import { wrapper } from './utils';

describe('useAuth', () => {
  beforeEach(() => {
    moxios.install();

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
    moxios.uninstall();
  });

  describe('login', () => {
    describe('given valid credentials', () => {
      test('sets auth token and fetches current account', async () => {
        moxios.stubRequest('/api/v1/accounts/login', {
          status: 200,
          response: { token: 'mock-token' },
        });

        const { result } = renderHook(() => useAuth(), { wrapper });

        expect(result.current.account).toBeNull();

        await act(() => result.current.login({ username: 'user', password: 'password' }));

        expect(axios.defaults.headers.common.Authorization).toEqual('Bearer mock-token');
        expect(result.current.account).not.toBeNull();
      });
    });

    describe('given invalid credentials', () => {
      test('account is null', async () => {
        moxios.stubRequest('/api/v1/accounts/login', {
          status: 401,
        });

        const { result } = renderHook(() => useAuth(), { wrapper });

        await act(() => result.current.login({ username: 'user', password: 'password' }).catch((error) => error));

        expect(result.current.account).toBeNull();
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

        const { result } = renderHook(() => useAuth(), { wrapper });

        expect(result.current.account).toBeNull();

        await act(() =>
          result.current.register({
            email: 'test@mail.com',
            name: 'Test',
            password: 'password',
            username: 'user',
          }),
        );

        expect(axios.defaults.headers.common.Authorization).toEqual('Bearer mock-token');
        expect(result.current.account).not.toBeNull();
      });
    });
  });

  describe('logout', () => {
    test('removes cookie and current account is null', async () => {
      document.cookie = 'token=mock-token;';

      const { result, waitForNextUpdate } = renderHook(() => useAuth(), { wrapper });

      await waitForNextUpdate();

      expect(result.current.account).not.toBeNull();

      act(() => result.current.logout());

      expect(document.cookie).toEqual('');
      expect(result.current.account).toBeNull();
    });
  });
});
