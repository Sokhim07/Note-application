import { createRouter, createWebHistory } from 'vue-router';
import Note_Form from '../pages/Note_Form.vue';
import Login from '../pages/Login.vue';
import User from '../pages/User_Form.vue';
import  useSessionStore  from '../stores/Session'; // use named import here

const routes = [
  {
    path: '/',
    name: 'Login',
    component: Login,
  },
  {
    path: '/notes',
    name: 'Notes',
    component: Note_Form,
    meta: { requiresAuth: true },
  },
  {
    path: '/user',
    name: 'User',
    component: User,
    meta: { requiresAuth: true },
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach((to, from, next) => {
  const session = useSessionStore(); 
  const isLoggedIn = !!session.USER_NAME;

  if (to.meta.requiresAuth && !isLoggedIn) {
    next({ name: 'Login' });
  } else {
    next();
  }
});

export default router;
