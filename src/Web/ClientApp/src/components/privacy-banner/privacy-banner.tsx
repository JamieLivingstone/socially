import { Button, Icon, Stack, Text } from '@chakra-ui/react';
import React from 'react';
import { useCookies } from 'react-cookie';
import { FaLock } from 'react-icons/fa';

export function PrivacyBanner() {
  const [cookies, setCookie] = useCookies(['privacyBannerAcknowledged']);

  if (cookies.privacyBannerAcknowledged) {
    return <></>;
  }

  return (
    <Stack
      p={4}
      boxShadow="lg"
      borderRadius="lg"
      bg="white"
      margin={2}
      position="fixed"
      bottom={0}
      left={0}
      right={0}
      zIndex={999}
    >
      <Stack direction="row" alignItems="center">
        <Text fontWeight="semibold">Your Privacy</Text>
        <Icon as={FaLock} />
      </Stack>

      <Stack direction={{ base: 'column', md: 'row' }} justifyContent="space-between">
        <Text fontSize={{ base: 'sm' }} textAlign="left" maxW="4xl">
          Socially uses cookies to provide necessary website functionality, improve your experience and analyze our
          traffic. By using our website, you agree to our Cookies Policy.
        </Text>

        <Stack direction={{ base: 'column', md: 'row' }}>
          <Button
            onClick={() =>
              setCookie('privacyBannerAcknowledged', true, {
                sameSite: 'strict',
                maxAge: 2419200, // 28 days
              })
            }
          >
            OK
          </Button>
        </Stack>
      </Stack>
    </Stack>
  );
}
