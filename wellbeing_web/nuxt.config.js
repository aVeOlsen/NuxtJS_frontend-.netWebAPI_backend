module.exports = {
  // Target: https://go.nuxtjs.dev/config-target
  target: 'static',
  // output: {
  //   hashFunction: 'xxhash64',
  // },
  // Global page headers: https://go.nuxtjs.dev/config-head
  head: {
    title: 'wellbeing_web',
    htmlAttrs: {
      lang: 'en',
    },
    meta: [
      { charset: 'utf-8' },
      { name: 'viewport', content: 'width=device-width, initial-scale=1' },
      { hid: 'description', name: 'description', content: '' },
      { name: 'format-detection', content: 'telephone=no' },
    ],
    link: [{ rel: 'icon', type: 'image/x-icon', href: '/favicon.ico' }],
  },

  watchers: {
    webpack: {
      poll: true,
      ignored: [
        '**/*.{md,log,prettierignore,prettierrrc,stylelintignore,npmrc,gitignore}',
        '**/*.eslintcache',
        '**/.git/**',
      ],
    },
  },

  // Global CSS: https://go.nuxtjs.dev/config-css
  css: ['@/assets/css/tailwind.css'],

  env: {
    baseUrl: process.env.BASE_URL || 'http://localhost:3000',
    apiURL:'https://iq0t7hfiqe.execute-api.eu-west-1.amazonaws.com/api',
    sanityUrl: 'https://wellbeing.sanity.studio/', //'http://localhost:3333',//
    jwtKey: 'jwtkey',
    netlifyAccess:'netlifyaccess'
  }, 
 

  // Plugins to run before rendering page: https://go.nuxtjs.dev/config-plugins
  plugins: ['@/plugins/sanity_imageBuilder.js', "@/plugins/password_encryptor.js"],
  // middleware: ['auth'],

  // Auto import components: https://go.nuxtjs.dev/config-components
  components: true,

  // Modules for dev and build (recommended): https://go.nuxtjs.dev/config-modules
  buildModules: [
    // https://go.nuxtjs.dev/tailwindcss
    // '@nuxtjs/tailwindcss',
    '@nuxt/postcss8',
    '@nuxtjs/sanity/module',
  ],

  // Modules: https://go.nuxtjs.dev/config-modules
  modules: [
    // https://go.nuxtjs.dev/axios
    '@nuxtjs/axios',
    '@nuxtjs/auth-next',
  ],

  // Axios module configuration: https://go.nuxtjs.dev/config-axios
  axios: {
    // Workaround to avoid enforcing hard-coded localhost:3000: https://github.com/nuxt-community/axios-module/issues/308
    baseURL: 'https://iq0t7hfiqe.execute-api.eu-west-1.amazonaws.com/api',//'http://localhost:5000/api'//
  },

  // src: https://auth.nuxtjs.org/guide/middleware/
  // Requires user to be logged in to access website
  router: {
    middleware: ['auth', 'auth-user'],
  },

  sanity: {
    projectId: 'c6jh1rru',
  },
  auth: {
    strategies: {
      local: {
        token: {
          property: 'token',
          global: true,
          required: true,
          type: 'Bearer',
        },
        redirect: {
          login: '/login',
          callback: '/login',
          logout: '/login',
        },
        user: {
          property: false,
          autoFetch: true,
        },
        endpoints: {
          login: {
            url: '/AppUser/authenticate',
            method: 'post',
            propertyName: 'token',
          },
          logout: false,
          user: false
          // {
          //   method: 'get', 
          //   url: '/AppUser/me',
          // },
        },
        watchLoggedIn: true,
      },
    },
  },

  // Build Configuration: https://go.nuxtjs.dev/config-build
  build: {
    postcss: {
      plugins: {
        tailwindcss: {},
        autoprefixer: {},
      },
    },
  },
}
