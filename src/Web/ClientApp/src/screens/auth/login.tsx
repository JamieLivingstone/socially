import { Box, Button, Heading, Link, Stack, Text } from '@chakra-ui/react';
import { Form, Formik } from 'formik';
import React, { useState } from 'react';
import { Link as BrowserLink } from 'react-router-dom';
import * as Yup from 'yup';

import TextField from '@components/text-field';
import { useAuth } from '@hooks/use-auth';

function Login() {
  const { login } = useAuth();
  const [error, setError] = useState<string | null>(null);

  return (
    <Stack spacing={8} alignItems="center">
      <Stack align="center">
        <Heading fontSize="3xl" as="h1">
          Login
        </Heading>

        <Text fontSize="lg">
          Don't have an account?{' '}
          <Link as={BrowserLink} to="/register" color="green">
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

                <TextField name="username" label="Username" placeholder="Username" isRequired />

                <TextField name="password" label="Password" type="password" placeholder="Password" isRequired />

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
