import { router } from "~/nuxt.config"

export default async function (context) {
  // context.userAgent = process.server
  // ? context.req.headers['user-agent']
  // : navigator.userAgent
  if (context) {
    const user = context.$auth.$state.user
    if (user) {
    }
  } else {
    router.push('/login')
  }
}
