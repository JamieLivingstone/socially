import { FormControl, FormErrorMessage, FormLabel, Textarea } from '@chakra-ui/react';
import { useField } from 'formik';
import React from 'react';

type TextAreaInputProps = React.ComponentPropsWithoutRef<'textarea'> & {
  name: string;
  label: string;
  isRequired?: boolean;
};

export function TextAreaInput({ name, label, isRequired = false, ...rest }: TextAreaInputProps) {
  const [field, meta] = useField<string>(name);

  return (
    <FormControl isInvalid={Boolean(meta.error) && meta.touched} isRequired={isRequired} mb={4}>
      <FormLabel htmlFor={name}>{label}</FormLabel>

      <Textarea {...field} {...rest} label={label} id={name} minH="250px" />

      <FormErrorMessage>{meta.error}</FormErrorMessage>
    </FormControl>
  );
}
