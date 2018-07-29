// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.

export const environment = {
  production: false,
  // identityServerBaseUri: 'https://localhost:44338',
  // apiBaseUri: 'https://localhost:44384',
  identityServerBaseUri: 'https://budgie-identity.azurewebsites.net',
  apiBaseUri: 'https://budgie-api.azurewebsites.net'
};
