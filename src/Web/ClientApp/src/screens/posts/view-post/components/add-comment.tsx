import { Box, Button } from '@chakra-ui/react';
import { Form, Formik } from 'formik';
import React from 'react';
import * as Yup from 'yup';

import { TextareaField } from '../../../../components';
import { useCreateComment } from '../hooks';

type AddCommentProps = {
  slug: string;
};

export function AddComment({ slug }: AddCommentProps) {
  const { createComment } = useCreateComment();

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
