import { Box, Button, Heading, Stack, Tab, TabList, TabPanel, TabPanels, Tabs } from '@chakra-ui/react';
import axios from 'axios';
import { Form, Formik } from 'formik';
import React from 'react';
import * as Yup from 'yup';

import Markdown from '@components/markdown';
import SelectField from '@components/select-field';
import TextField from '@components/text-field';
import TextareaField from '@components/textarea-field';

import { useTagList } from '../common/hooks/use-tag-list';
import { useCreatePost } from './hooks/use-create-post';

function CreatePost() {
  const { createPost } = useCreatePost();
  const tagList = useTagList();

  return (
    <Box rounded="lg" border={1} bg="white" boxShadow="lg" p={4} width="100%">
      <Heading fontSize="2xl" as="h1" mb={4}>
        Create Post
      </Heading>

      <Stack spacing={4}>
        <Formik
          initialValues={{
            title: '',
            body: '',
            tags: [],
          }}
          validationSchema={Yup.object().shape({
            title: Yup.string().required('title is a required field').max(150),
            body: Yup.string().required('body is a required field'),
            tags: Yup.array()
              .of(Yup.string())
              .required('tags is a required field')
              .min(1, 'posts require a minimum of one tag'),
          })}
          onSubmit={async (values, { setSubmitting, setFieldError }) => {
            try {
              await createPost(values);
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
                    <TextField name="title" label="Title" placeholder="Title" isRequired />

                    <TextareaField
                      name="body"
                      label="Body"
                      placeholder="Write post body in markdown format"
                      minH="250px"
                      isRequired
                    />

                    <SelectField
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
                  <Markdown source={`${values.title && `# ${values.title}\n`}${values.body}`} />
                </TabPanel>
              </TabPanels>
            </Tabs>
          )}
        </Formik>
      </Stack>
    </Box>
  );
}

export default CreatePost;
