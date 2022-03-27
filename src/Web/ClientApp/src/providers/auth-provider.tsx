import React, { ReactNode } from 'react';

import Loading from '@components/loading';
import { AuthContext, useProvideAuth } from '@hooks/use-auth';

export function AuthProvider({ children }: { children: ReactNode }) {
  const auth = useProvideAuth();

  if (auth.initializing) {
    return <Loading />;
  }

  return <AuthContext.Provider value={auth}>{children}</AuthContext.Provider>;
}
