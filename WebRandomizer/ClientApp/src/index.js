/* Polyfill features missing from IE11 and Edge */
import 'react-app-polyfill/ie11';
import 'react-app-polyfill/stable';
import './polyfill/TextDecoder';

import 'bootstrap/dist/css/bootstrap.css';

import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter as Router } from 'react-router-dom';

import App from './App';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');

ReactDOM.render(
    <Router basename={baseUrl}>
        <App />
    </Router>,
    document.getElementById('root')
);

// import registerServiceWorker from './registerServiceWorker';

// Don't use a service worker while we're in heavy development since using it caches resources in a way that we don't want.
// Not sure if this is ever a good idea to use for this project, we'll see.
// registerServiceWorker();
