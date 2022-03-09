import { Box, Button, Heading, Link, Stack, Text } from '@chakra-ui/react';
import axios from 'axios';
import { Form, Formik } from 'formik';
import React from 'react';
import { Link as BrowserLink } from 'react-router-dom';
import * as Yup from 'yup';

import { TextField } from '../../../components';
import { routes } from '../../../constants';
import { useAuth } from '../../../hooks';

function Register() {
  const { register } = useAuth();

  return (
    <Stack spacing={8} alignItems="center">
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
                <TextField name="name" label="Name" placeholder="Full Name" isRequired />

                <TextField name="username" label="Username" placeholder="Username" isRequired />

                <TextField name="email" label="Email" placeholder="Email" isRequired />

                <TextField name="password" label="Password" type="password" placeholder="Password" isRequired />

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
