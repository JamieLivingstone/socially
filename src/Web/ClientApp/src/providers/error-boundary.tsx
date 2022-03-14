import axios from 'axios';
import React, { Component } from 'react';

import { Error } from '../screens/miscellaneous/error';
import { NotFound } from '../screens/miscellaneous/not-found';

type ErrorBoundaryProps = {
  children: React.ReactNode;
};

type ErrorBoundaryState = {
  hasError: boolean;
  error: Error | null;
};

export class ErrorBoundary extends Component<ErrorBoundaryProps, ErrorBoundaryState> {
  constructor(props: ErrorBoundaryProps) {
    super(props);
    this.state = { hasError: false, error: null };
  }

  static getDerivedStateFromError(error: Error) {
    return { hasError: true, error };
  }

  render() {
    if (this.state.hasError) {
      if (axios.isAxiosError(this.state.error) && this.state.error.response?.status === 404) {
        return <NotFound />;
      }

      return <Error />;
    }

    return this.props.children;
  }
}
