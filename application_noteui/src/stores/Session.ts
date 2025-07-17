import { defineStore } from "pinia";
import { ref } from "vue";

const useSessionStore = defineStore(
  "session",
  () => {
    const USER_NAME = ref("");

    return {
      USER_NAME,
    };
  },
);

export default useSessionStore;
