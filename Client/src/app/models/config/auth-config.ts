import { AuthConfig } from 'angular-oauth2-oidc';

export const authConfig: AuthConfig = {
	// issuer: 'http://localhost:5000',
	// redirectUri: window.location.origin + '/home',
	// clientId: 'spa',
	// scope: 'openid profile email api.fullAccess',

	issuer: 'http://localhost:5000',
	redirectUri: window.location.origin + '/home',
	clientId: 'angular-client',
	scope: 'openid profile email api.fullAccess',
};
