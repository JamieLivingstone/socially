import axios from 'axios';
import { useQuery } from 'react-query';

import { Profile } from '../types';

export function useProfile(username: string) {
  const { data } = useQuery(
    ['profile', username],
    async function fetchProfileRequest() {
      const { data } = await axios.get<Profile>(`/api/v1/profiles/${username}`);

      return data;
    },
    {
      suspense: true,
      useErrorBoundary: true,
    },
  );

  return {
    profile: data as Profile,
  };
}
