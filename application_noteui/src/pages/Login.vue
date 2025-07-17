<template>


  <div class="flex items-center justify-center min-h-screen bg-gray-100">
    <div class="bg-white p-8 rounded-lg shadow-md w-full max-w-sm">
      <h2 class="text-2xl font-bold mb-6 text-center">Login</h2>
      <form @submit.prevent="handleSubmit">
        <div class="mb-4">
          <label class="block text-sm font-medium mb-2">User Name</label>
          <input
            v-model="dto.USER_NAME"
            type="text"
            class="w-full px-3 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
            placeholder="Enter your user"
            required
          />
        </div>
        <div class="mb-6">
          <label class="block text-sm font-medium mb-2" for="password">Password</label>
          <input
            v-model="dto.PASSWORD"
            type="password"
            id="password"
            class="w-full px-3 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
            placeholder="Enter your password"
            required
          />
        </div>
        <button
          type="submit"
          class="w-full bg-blue-500 text-white py-2 rounded-lg hover:bg-blue-600 focus:outline-none focus:ring-2 focus:ring-blue-500"
        >
          Login
        </button>
      </form>
    </div>
  </div>
</template>

<script setup lang="ts">
import { reactive} from 'vue';
import router from '../router';
import $api from '../utils/api';
import $msg from '../utils/helper';
import useSessionStore from '../stores/Session';
const $session = useSessionStore();

interface LoginForm {
  USER_NAME: string
  PASSWORD: string
}

const dto = reactive<LoginForm>({
  USER_NAME: '',
  PASSWORD: ''
})

const handleSubmit = () => {
  $api.get("http://localhost:5094/api/user/login", dto, (data) => {
    if(data) {
      $session.USER_NAME = data[0].USER_NAME;
     router.push('/notes');
    }
}, () => {
  $msg.error("Login Faild");
});
 
}
</script>
