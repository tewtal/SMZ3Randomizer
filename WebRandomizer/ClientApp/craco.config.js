const path = require('path');

const webpackPlugin = {
    plugin: {
        overrideWebpackConfig: ({webpackConfig}) => {
            const wasmExtensionRegExp = /\.wasm$/;
            webpackConfig.module.rules.forEach((rule) => {
                (rule.oneOf || []).forEach((oneOf) => {
                    if (oneOf.loader && oneOf.loader.indexOf('file-loader') >= 0) {
                        // Make file-loader ignore WASM files
                        oneOf.exclude.push(wasmExtensionRegExp);
                    }
                });
            });

            webpackConfig.module.rules.push({
                test: wasmExtensionRegExp,
                include: path.resolve(__dirname, 'src'),
                use: [{ loader: require.resolve('wasm-loader'), options: {} }]
            });

            return webpackConfig;
        },
    },
};

module.exports = function({env}) {
    return {
        plugins: [webpackPlugin],
    };
};