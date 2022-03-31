import { Box, Button, Link, Text } from '@chakra-ui/react';
import { Form, Formik } from 'formik';
import React from 'react';
import { Link as BrowserLink, useLocation } from 'react-router-dom';
import * as Yup from 'yup';

import TextareaField from '@components/textarea-field';
import { useAuth } from '@hooks/use-auth';

import { useCreateComment } from '../hooks/use-create-comment';

type AddCommentProps = {
  slug: string;
};

function AddComment({ slug }: AddCommentProps) {
  const { account } = useAuth();
  const { createComment } = useCreateComment();
  const location = useLocation();

  if (!account) {
    return (
      <Text my={4}>
        <Link as={BrowserLink} to={`/login?redirect=${location.pathname}`} color="green">
          Login
        </Link>{' '}
        or{' '}
        <Link as={BrowserLink} to={`/register?redirect=${location.pathname}`} color="green">
          register
        </Link>{' '}
        to add comments on this post.
      </Text>
    );
  }

  return (
    <Formik
      initialValues={{
        message: '',
      }}
      validationSchema={Yup.object().shape({
        message: Yup.string().required('message is a required field').min(5).max(500),
      })}
      onSubmit={async (values, { setSubmitting, resetForm }) => {
        try {
          await createComment({ message: values.message, slug });
          resetForm();
        } finally {
          setSubmitting(false);
        }
      }}
    >
      {({ isSubmitting }) => (
        <Box my={4}>
          <Form noValidate>
            <TextareaField name="message" placeholder="Add to the discussion" isRequired minH="75px" />

            <Button type="submit" disabled={isSubmitting} variant="solid">
              Add Comment
            </Button>
          </Form>
        </Box>
      )}
    </Formik>
  );
}

export default AddComment;
