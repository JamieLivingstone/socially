import { Box, Button, Heading, Stack, Tab, TabList, TabPanel, TabPanels, Tabs } from '@chakra-ui/react';
import MDEditor from '@uiw/react-md-editor';
import axios from 'axios';
import { Form, Formik } from 'formik';
import React from 'react';
import sanitize from 'rehype-sanitize';
import * as Yup from 'yup';

import { SelectInput, TextAreaInput, TextInput } from '../components';
import { useCreatePost, useTagList } from '../hooks';

function CreatePost() {
  const createPost = useCreatePost();
  const tagList = useTagList();

  return (
    <Stack spacing={8} py={12} px={6} mx="auto" width="100%" alignItems="center">
      <Stack align="center">
        <Heading fontSize="3xl" as="h1">
          Create Post
        </Heading>
      </Stack>

      <Box rounded="lg" bg="white" boxShadow="lg" p={8} width="800px" maxWidth="100%">
        <Stack spacing={4}>
          <Formik
            initialValues={{
              title: '',
              body: '',
              tags: [],
            }}
            validationSchema={Yup.object().shape({
              title: Yup.string().required('title is a required field'),
              body: Yup.string().required('body is a required field'),
              tags: Yup.array().of(Yup.string()),
            })}
            onSubmit={async (values, { setSubmitting, setFieldError }) => {
              try {
                await createPost.mutateAsync(values);
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
            {({ isSubmitting, values }) => (
              <Tabs>
                <TabList>
                  <Tab>Edit</Tab>
                  <Tab>Preview</Tab>
                </TabList>

                <TabPanels>
                  <TabPanel>
                    <Form noValidate>
                      <TextInput name="title" label="Title" placeholder="Title" isRequired />

                      <TextAreaInput
                        name="body"
                        label="Body"
                        placeholder="Write post body in markdown format"
                        isRequired
                      />

                      <SelectInput
                        name="tags"
                        label="Tags"
                        placeholder="Tags"
                        isLoading={tagList.isLoading}
                        onInputChange={tagList.setFilter}
                        options={tagList.tags.map(({ name }) => ({ label: name, value: name }))}
                        isRequired
                      />

                      <Button mt={2} isFullWidth type="submit" disabled={isSubmitting} variant="solid">
                        Create Post
                      </Button>
                    </Form>
                  </TabPanel>

                  <TabPanel>
                    <MDEditor.Markdown
                      source={`${values.title && `# ${values.title}\n`}${values.body}`}
                      plugins={[sanitize]}
                    />
                  </TabPanel>
                </TabPanels>
              </Tabs>
            )}
          </Formik>
        </Stack>
      </Box>
    </Stack>
  );
}

export default CreatePost;
