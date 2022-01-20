import { FormControl, FormErrorMessage, FormLabel, Input } from '@chakra-ui/react';
import { useField } from 'formik';
import React from 'react';

export type InputProps = {
  name: string;
};

type TextInputProps = Omit<React.ComponentPropsWithoutRef<'input'>, 'size'> & {
  name: string;
  label: string;
  isRequired?: boolean;
};

export function TextInput({ name, label, isRequired = false, ...rest }: TextInputProps) {
  const [field, meta] = useField(name);

  return (
    <FormControl isInvalid={Boolean(meta.error) && meta.touched} isRequired={isRequired} mb={4}>
      <FormLabel htmlFor={name}>{label}</FormLabel>
      <Input {...field} {...rest} label={label} id={name} />
      <FormErrorMessage>{meta.error}</FormErrorMessage>
    </FormControl>
  );
}
