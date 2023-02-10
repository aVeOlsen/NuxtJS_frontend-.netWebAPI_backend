const state = { id:'', questionTitle:'' }

const getters = {
  isAuthenticated(state) {
    return state.auth.loggedIn
  },

  loggedInUser(state) {
    return state.auth.user
  },
  getWellbeingId: (state) => state.id,
  getQuestionTitle:(state)=>state.questionTitle
}
const mutations = {
  setUser(state, user) {
    state.auth.user = user
    // return state.auth.user
  },
  setWellbeingId(state, id) {
    state.id = id
  },
  setQuestionTitle(state, questionTitle){
    state.questionTitle=questionTitle
  }
}
const actions = {
  setWellbeingId({commit}, id){
    commit('setWellbeingId', id)
  },
  setQuestionTitle({commit}, questionTitle){
    commit('setQuestionTitle', questionTitle)
  }
}

export default {
  getters,
  mutations,
  actions, 
  state
}
// VueX Store -> Mutations = setters. se nærmere. kig på https://www.google.com/search?q=nuxt+auth+example&oq=nuxt+auth+example&aqs=chrome.0.0i512l2j0i22i30l8.1774j0j4&sourceid=chrome&ie=UTF-8

// const store = new VueX.Store({
//   state: {},

//   mutations: {},

//   actions: {},

//   getters: {},
// })
