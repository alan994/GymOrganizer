import { AuthConfig } from 'angular-oauth2-oidc';

export const authConfig: AuthConfig = {
	issuer: 'http://localhost:5000',
	redirectUri: window.location.origin + '/loading',
	clientId: 'angular-client',
	scope: 'openid profile email api.fullAccess',
	silentRefreshRedirectUri: window.location.origin + '/silent-refresh.html',
	timeoutFactor: 0.1
};
