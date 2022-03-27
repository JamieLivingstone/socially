import { Box, Button, Heading, Stack, Tab, TabList, TabPanel, TabPanels, Tabs } from '@chakra-ui/react';
import axios from 'axios';
import { Form, Formik } from 'formik';
import React from 'react';
import { useParams } from 'react-router-dom';
import * as Yup from 'yup';

import Markdown from '@components/markdown';
import SelectField from '@components/select-field';
import TextField from '@components/text-field';
import TextareaField from '@components/textarea-field';

import { usePost } from '../common/hooks/use-post';
import { useTagList } from '../common/hooks/use-tag-list';
import { useEditPost } from './hooks';

function EditPost() {
  const { slug } = useParams();
  const { post } = usePost(slug ?? '');
  const { editPost } = useEditPost();
  const tagList = useTagList();

  return (
    <Stack spacing={8} alignItems="center">
      <Stack align="center">
        <Heading fontSize="3xl" as="h1">
          Edit Post
        </Heading>
      </Stack>

      <Box rounded="lg" bg="white" boxShadow="lg" p={8} width="800px" maxWidth="100%">
        <Stack spacing={4}>
          <Formik
            initialValues={{
              title: post.title,
              body: post.body,
              slug: post.slug,
              tags: post.tags.map((tag) => tag.name),
            }}
            validationSchema={Yup.object().shape({
              title: Yup.string().required('title is a required field'),
              body: Yup.string().required('body is a required field'),
              tags: Yup.array().of(Yup.string()),
            })}
            onSubmit={async (values, { setSubmitting, setFieldError }) => {
              try {
                await editPost(values);
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
                        Edit Post
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
    </Stack>
  );
}

export default EditPost;
