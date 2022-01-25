import { FormControl, FormErrorMessage, FormLabel } from '@chakra-ui/react';
import { CreatableSelect } from 'chakra-react-select';
import { useField } from 'formik';
import React from 'react';

type SelectInputProps = {
  name: string;
  label: string;
  placeholder?: string;
  isRequired?: boolean;
  isMulti?: boolean;
  isLoading?: boolean;
  onInputChange?: (input: string) => void;
  options: Array<{ label: string; value: string }>;
};

export function SelectInput({
  name,
  label,
  isRequired = false,
  isMulti = true,
  onInputChange,
  ...rest
}: SelectInputProps) {
  const [, meta, helpers] = useField<Array<string>>(name);

  return (
    <FormControl isInvalid={Boolean(meta.error) && meta.touched} isRequired={isRequired} mb={4}>
      <FormLabel htmlFor={name}>{label}</FormLabel>

      <CreatableSelect
        {...rest}
        id={name}
        isMulti={isMulti}
        onInputChange={onInputChange}
        onChange={(selected) => {
          if (Array.isArray(selected)) {
            helpers.setValue(selected.map((option) => option.value));
          }
        }}
        components={{
          DropdownIndicator: () => null,
        }}
      />

      <FormErrorMessage>{meta.error}</FormErrorMessage>
    </FormControl>
  );
}
