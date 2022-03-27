import { Button } from '@chakra-ui/react';
import { act, fireEvent, render, waitFor } from '@testing-library/react';
import { Form, Formik } from 'formik';
import React from 'react';
import * as Yup from 'yup';

import TextField from '@components/text-field';
import TextareaField from '@components/textarea-field';

describe('<Form />', () => {
  test('rendering and submitting a basic form', async () => {
    const onSubmit = vi.fn();

    const { getByRole, getByLabelText } = await render(
      <Formik
        initialValues={{
          username: '',
          bio: '',
        }}
        onSubmit={(values) => onSubmit(values)}
      >
        <Form noValidate>
          <TextField name="username" label="Username" />

          <TextareaField name="bio" label="Bio" />

          <Button type="submit" variant="solid">
            Submit
          </Button>
        </Form>
      </Formik>,
    );

    act(() => {
      fireEvent.change(getByLabelText(/username/i), { target: { value: 'john' } });
      fireEvent.change(getByLabelText(/bio/i), { target: { value: 'custom bio' } });
      fireEvent.click(getByRole('button'));
    });

    await waitFor(() => {
      expect(onSubmit).toHaveBeenCalledTimes(1);
      expect(onSubmit).toHaveBeenCalledWith({ username: 'john', bio: 'custom bio' });
    });
  });

  test('validates form', async () => {
    const { getByLabelText, queryByText } = await render(
      <Formik
        initialValues={{
          username: '',
        }}
        validationSchema={Yup.object({
          username: Yup.string().required('Username is a required field'),
        })}
        onSubmit={vi.fn()}
      >
        <Form noValidate>
          <TextField name="username" label="Username" />
        </Form>
      </Formik>,
    );

    expect(queryByText(/username is a required field/i)).toBeNull();

    fireEvent.blur(getByLabelText(/username/i));

    await waitFor(() => {
      expect(queryByText(/username is a required field/i)).not.toBeNull();
    });
  });
});
