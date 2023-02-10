// First, we must import the schema creator
import createSchema from 'part:@sanity/base/schema-creator'

// Then import schema types from any plugins that might expose them
import schemaTypes from 'all:part:@sanity/base/schema-type'

// Then we give our schema to the builder and provide the result to Sanity
export default createSchema({
  // We name our schema
  name: 'default',
  // Then proceed to concatenate our document type
  // to the ones provided by any plugins that are installed
  types: schemaTypes.concat([
    /* Your types here! */
    {
      title: 'NewsFeed',
      name: 'newsfeed',
      type: 'document',
      fields: [
        {
          title: 'Titel',
          name: 'title',
          type: 'string',
          description: 'Tilføj titlen her',
        },
        {
          title: 'Nyheds cover',
          name: 'newscover',
          type: 'image',
          description: 'Upload nyheds cover image her',
          options: {
            hotspot: true,
          },
        },
        {
          title: 'Content',
          name: 'content',
          type: 'string',
          description: 'Tilføj brødtekst her',
        },
        {
          title: 'Start periode',
          name: 'startPeriod',
          type: 'date',
          description: 'Tilføj start dato her',
        },
        {
          title: 'Slut periode',
          name: 'endPeriod',
          type: 'date',
          description: 'Tilføj slut dato her',
        },
        {
          title: 'Notes',
          name: 'notes',
          type: 'array',
          description:
            'Type notes as you learn something new about the case here',
          of: [{ type: 'block' }, { type: 'string' }],
        },
        {
          name: 'slug',
          title: 'Slug',
          type: 'slug',
          options: {
            source: '_id',
            maxLength: 96,
          },
        },
      ],
    },
    {
      title: 'Spørgeskema',
      name: 'questionnaire',
      type: 'document',
      fields: [
        {
          title: 'Titel',
          name: 'title',
          type: 'string',
          description: 'Tilføj titlen her',
        },
        {
          title: 'Spørgeskema cover',
          name: 'coverimage',
          type: 'image',
          description: 'Upload cover billed her',
          options: {
            hotspot: true,
          },
        },
        {
          title: 'Beskrivelse',
          name: 'description',
          type: 'string',
          description: 'Tilføj beskrivelsen her',
        },
        {
          title: 'Spørgsmål liste',
          name: 'questionList',
          type: 'array',
          of: [{ type: 'string' }],
          validation: (Rule) => Rule.unique(),
        },
        {
          name: 'slug',
          title: 'Slug',
          type: 'slug',
          options: {
            source: '_id',
            maxLength: 96,
          },
        },
      ],
    },
  ]),
})
