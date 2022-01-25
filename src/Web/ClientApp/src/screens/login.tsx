import { Box, Button, Center, Heading, Link, Stack, Text } from '@chakra-ui/react';
import { Form, Formik } from 'formik';
import React, { useEffect, useState } from 'react';
import { Link as BrowserLink, useNavigate } from 'react-router-dom';
import * as Yup from 'yup';

import { TextInput } from '../components';
import { routes } from '../constants';
import { useAuth } from '../hooks';

function Login() {
  const { login, account } = useAuth();
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  useEffect(() => {
    if (account) {
      navigate(routes.HOME, { replace: true });
    }
  }, [account]);

  return (
    <Stack spacing={8} py={12} px={6} mx="auto" width="100%" alignItems="center">
      <Stack align="center">
        <Heading fontSize="3xl" as="h1">
          Login
        </Heading>

        <Text fontSize="lg">
          Don't have an account?{' '}
          <Link as={BrowserLink} to={routes.REGISTER} color="green">
            Register
          </Link>{' '}
        </Text>
      </Stack>

      <Box rounded="lg" bg="white" boxShadow="lg" p={8} width="500px" maxWidth="100%">
        <Stack spacing={4}>
          <Formik
            initialValues={{
              username: '',
              password: '',
            }}
            validationSchema={Yup.object().shape({
              username: Yup.string().required('username is a required field'),
              password: Yup.string().required('password is a required field'),
            })}
            onSubmit={async (values, { setSubmitting }) => {
              try {
                if (error) {
                  setError(null);
                }

                await login(values);
              } catch (error) {
                setSubmitting(false);
                setError('invalid username / password combination');
              }
            }}
          >
            {({ isSubmitting }) => (
              <Form noValidate>
                {error && (
                  <Text color="red" mb={4}>
                    {error}
                  </Text>
                )}

                <TextInput name="username" label="Username" placeholder="Username" isRequired />

                <TextInput name="password" label="Password" type="password" placeholder="Password" isRequired />

                <Button mt={2} isFullWidth type="submit" disabled={isSubmitting} variant="solid">
                  Login
                </Button>
              </Form>
            )}
          </Formik>
        </Stack>
      </Box>
    </Stack>
  );
}

export default Login;
