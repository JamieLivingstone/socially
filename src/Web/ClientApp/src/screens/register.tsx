import { Box, Button, Heading, Link, Stack, Text } from '@chakra-ui/react';
import axios from 'axios';
import { Form, Formik } from 'formik';
import React, { useEffect } from 'react';
import { Link as BrowserLink, useNavigate } from 'react-router-dom';
import * as Yup from 'yup';

import { TextInput } from '../components/form-inputs';
import { routes } from '../constants';
import { useAuth } from '../hooks/use-auth';

function Register() {
  const { register, account } = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    if (account) {
      navigate(routes.HOME, { replace: true });
    }
  }, [account]);

  return (
    <Stack spacing={8} mx="2" py={12} alignItems="center">
      <Stack align="center">
        <Heading fontSize="3xl" as="h1">
          Register
        </Heading>

        <Text fontSize="lg">
          Already have an account?{' '}
          <Link as={BrowserLink} to={routes.LOGIN} color="green">
            Login
          </Link>{' '}
        </Text>
      </Stack>

      <Box rounded="lg" bg="white" boxShadow="lg" p={8} width="500px" maxWidth="100%">
        <Stack spacing={4}>
          <Formik
            initialValues={{
              name: '',
              username: '',
              email: '',
              password: '',
            }}
            validationSchema={Yup.object().shape({
              name: Yup.string().required(),
              username: Yup.string().required(),
              email: Yup.string().email().required(),
              password: Yup.string()
                .min(8)
                .matches(/[a-z]/, 'password must contain a lowercase letter')
                .matches(/[A-Z]/, 'password must contain a uppercase letter')
                .required(),
            })}
            onSubmit={async (values, { setSubmitting, setFieldError }) => {
              try {
                await register(values);
              } catch (error) {
                if (axios.isAxiosError(error)) {
                  const errors = error.response?.data?.errors ?? {};

                  Object.keys(errors).map((error) => {
                    setFieldError(error, errors[error][0]);
                  });
                }

                setSubmitting(false);
              }
            }}
          >
            {({ isSubmitting }) => (
              <Form noValidate>
                <TextInput name="name" label="Name" isRequired />

                <TextInput name="username" label="Username" isRequired />

                <TextInput name="email" label="Email" isRequired />

                <TextInput name="password" label="Password" type="password" isRequired />

                <Button mt={2} isFullWidth type="submit" disabled={isSubmitting} variant="solid">
                  Register
                </Button>
              </Form>
            )}
          </Formik>
        </Stack>
      </Box>
    </Stack>
  );
}

export default Register;
