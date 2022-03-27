import { FormControl, FormErrorMessage, FormLabel, Textarea } from '@chakra-ui/react';
import { TextareaProps } from '@chakra-ui/react';
import { useField } from 'formik';
import React from 'react';

type TextareaFieldProps = TextareaProps & {
  name: string;
  label?: string;
  isRequired?: boolean;
};

function TextareaField({ name, label, isRequired = false, ...rest }: TextareaFieldProps) {
  const [field, meta] = useField<string>(name);

  return (
    <FormControl isInvalid={Boolean(meta.error) && meta.touched} isRequired={isRequired} mb={4}>
      {label && <FormLabel htmlFor={name}>{label}</FormLabel>}

      <Textarea {...field} {...rest} label={label} id={name} />

      <FormErrorMessage>{meta.error}</FormErrorMessage>
    </FormControl>
  );
}

export default TextareaField;
