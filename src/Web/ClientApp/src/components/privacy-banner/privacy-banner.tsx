import { LockIcon } from '@chakra-ui/icons';
import { Button, Stack, Text } from '@chakra-ui/react';
import React from 'react';
import { useCookies } from 'react-cookie';

export function PrivacyBanner() {
  const [cookies, setCookie] = useCookies(['privacyBannerAcknowledged']);

  if (cookies.privacyBannerAcknowledged) {
    return <></>;
  }

  return (
    <Stack
      p={4}
      boxShadow="lg"
      borderRadius="sm"
      bg="white"
      margin={2}
      position="fixed"
      bottom={0}
      left={0}
      right={0}
    >
      <Stack direction="row" alignItems="center">
        <Text fontWeight="semibold">Your Privacy</Text>
        <LockIcon />
      </Stack>

      <Stack direction={{ base: 'column', md: 'row' }} justifyContent="space-between">
        <Text fontSize={{ base: 'sm' }} textAlign="left" maxW="4xl">
          Socially uses cookies to provide necessary website functionality, improve your experience
          and analyze our traffic. By using our website, you agree to our Cookies Policy.
        </Text>

        <Stack direction={{ base: 'column', md: 'row' }}>
          <Button onClick={() => setCookie('privacyBannerAcknowledged', true)}>OK</Button>
        </Stack>
      </Stack>
    </Stack>
  );
}
