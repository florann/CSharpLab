import { defineConfig } from '@hey-api/openapi-ts';

export default defineConfig({
  input: 'http://localhost:5242/openapi/v1.json',
  output: './src/app/core/api',
  plugins: [
    '@hey-api/typescript', // Generates the types
    {
      name: '@hey-api/sdk', // This is what generates the service logic
      asClass: true,        // This creates the Classes (UserService, etc.)
    },
  ],
});