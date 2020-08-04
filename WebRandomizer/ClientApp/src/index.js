/* Polyfill features missing from IE11 and Edge */
import 'react-app-polyfill/ie11';
import 'react-app-polyfill/stable';
import './polyfill/TextDecoder';

import 'bootstrap/dist/css/bootstrap.css';
import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import App from './App';
// import registerServiceWorker from './registerServiceWorker';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');

ReactDOM.render(
    <BrowserRouter basename={baseUrl}>
        <App />
    </BrowserRouter>,
    rootElement);

// Don't use a service worker while we're in heavy development since using it caches resources in a way that we don't want.
// Not sure if this is ever a good idea to use for this project, we'll see.
// registerServiceWorker();
