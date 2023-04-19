const path = require('path');
const webpack = require('webpack');

const webpackPlugin = {
    plugin: {
        overrideWebpackConfig: ({webpackConfig}) => {
            const wasmExtensionRegExp = /\.wasm$/
            webpackConfig.resolve.extensions.push('.wasm')
            webpackConfig.experiments = {
                syncWebAssembly: true,
            }

            webpackConfig.module.rules.forEach(rule => {
                ; (rule.oneOf || []).forEach(oneOf => {
                    if (oneOf.type === 'asset/resource') {
                        oneOf.exclude.push(wasmExtensionRegExp)
                    }
                })
            })

            webpackConfig.plugins.push(
                new webpack.ProvidePlugin({
                    Buffer: ['buffer', 'Buffer']
                })
            )

            webpackConfig.resolve.fallback = { "path": require.resolve("path-browserify"), "buffer": require.resolve("buffer"), "util": require.resolve("util/") };

            return webpackConfig;
        },
    },
};

module.exports = function({env}) {
    return {
        plugins: [webpackPlugin],
    };
};