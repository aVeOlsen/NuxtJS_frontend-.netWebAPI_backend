import sanityClient from '@sanity/client'
export default sanityClient({
  projectId: 'c6jh1rru',
  dataset: 'production',
  apiVersion: '2021-08-31',
  useCdn: true,
})
