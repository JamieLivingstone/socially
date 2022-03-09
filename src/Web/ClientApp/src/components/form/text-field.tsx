import { FormControl, FormErrorMessage, FormLabel, Input } from '@chakra-ui/react';
import { useField } from 'formik';
import React from 'react';

type TextFieldProps = Omit<React.ComponentPropsWithoutRef<'input'>, 'size'> & {
  name: string;
  label: string;
  isRequired?: boolean;
};

export function TextField({ name, label, isRequired = false, ...rest }: TextFieldProps) {
  const [field, meta] = useField(name);

  return (
    <FormControl isInvalid={Boolean(meta.error) && meta.touched} isRequired={isRequired} mb={4}>
      <FormLabel htmlFor={name}>{label}</FormLabel>
      <Input {...field} {...rest} label={label} id={name} />
      <FormErrorMessage>{meta.error}</FormErrorMessage>
    </FormControl>
  );
}
