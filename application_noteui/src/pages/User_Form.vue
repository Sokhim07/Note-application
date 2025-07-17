<template>
  <Header/>
  <div v-show="$view =='VIEW'" class="w-full h-screen bg-gray-50 p-6">
    <div class="flex items-center justify-between mb-4">
      <h1 class="text-2xl font-semibold text-gray-800">User</h1>
      <button
        class="bg-green-500 hover:bg-green-600 text-white px-4 py-2 rounded-md text-sm flex items-center gap-2" @click="addnew()"
      >
        <span>+</span> New
      </button>
    </div>
    <div class="flex mb-4">
      <input
        type="text"
        v-model="dto.FILTER"
         @keyup.enter="viewdata();"
        placeholder="Search..."
        class="border border-gray-300 px-4 py-2 w-full rounded-l-md focus:outline-none focus:ring-2 focus:ring-blue-400"
      />
      <button @click="viewdata" class="bg-blue-500 hover:bg-blue-600 text-white px-4 py-2 rounded-r-md flex items-center">
    <MagnifyingGlassIcon class="w-5 h-5" />
  </button>
    </div>

    <!-- User Table -->
    <div  class="overflow-auto h-[calc(100vh-180px)]" >
      <table class="min-w-full text-sm border-separate border-spacing-y-2">
        <thead class="bg-gray-100 text-gray-700 font-semibold text-left sticky top-0 z-10">
          <tr>
            <th class="px-4 py-2">No</th>
            <th class="px-4 py-2">User Name</th>
            <th class="px-4 py-2">Action</th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="(x, i) in listView"
            :key="x.US_ID"
            class="hover:bg-gray-50"
          >
            <td class="px-4 py-2">{{ i + 1 }}</td>
            <td class="px-4 py-2 capitalize">{{ x.USER_NAME }}</td>
           
            <td class="px-4 py-2 text-lg flex space-x-3">
            <EyeIcon
              class="w-5 h-5 text-blue-600 hover:text-blue-800 cursor-pointer"
              @click="viewdetail(x)"
              title="View"
            />
            
          </td>
          </tr>
        </tbody>
      </table>
    </div>
    
  </div>
  <div v-show="$view =='CREATE'" class="max-w p-12 bg-white">
   

    <div class="mb-4">
      <label class="block text-sm font-medium text-gray-700 mb-1">User Name</label>
      <input
      
        type="text"
        v-model="dto.USER_NAME"
        placeholder="Enter user"
        :disabled="disable"
        class="w-full px-4 py-2 border border-gray-300 rounded-md shadow-sm focus:ring-blue-500 focus:border-blue-500"
      />
    </div>

    <div class="mb-4">
      <label class="block text-sm font-medium text-gray-700 mb-1">Password</label>
      <input
      type="password"
      v-model="dto.PASSWORD"
      placeholder="Enter password"
      :disabled="disable"
      class="w-full px-4 py-2 border border-gray-300 rounded-md shadow-sm focus:ring-blue-500 focus:border-blue-500"
    />
    </div>

    <div class="text-right">
      <button
    @click="Cancel"
    class="bg-blue-600 text-white py-2 px-4 rounded-md hover:bg-blue-700 transition me-2"
    >
    Cancel
    </button>
    <button
    @click="savedata"
    v-show="!botton"
    class="bg-blue-600 text-white py-2 px-4 rounded-md hover:bg-blue-700 transition me-2"
    >
    Save 
    </button>
    <button
    @click="Edit"
    v-show="botton"
    class="bg-blue-600 text-white py-2 px-4 rounded-md hover:bg-blue-700 transition me-2"
    >
    Edit
    </button>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref,onMounted, reactive} from 'vue'
import $api from '../utils/api'
import Header from '../components/header.vue';
import $msg from '../utils/helper';
import { EyeIcon,MagnifyingGlassIcon  } from '@heroicons/vue/24/outline'
const $view = ref("VIEW");

interface list {
  US_ID: number,
  USER_NAME: string,
  PASSWORD: string,
}
interface Dtos {
  US_ID: number,
  USER_NAME: string,
  PASSWORD: string,
  FILTER: string,
  IS_LOCK:boolean,
  LLOG_IN:string,
  IS_ONLINE: string,
  LLOG_OUT: string,
}
const dto = reactive<Dtos>({
  US_ID:-1,
  USER_NAME: "",
  PASSWORD:"",
  FILTER:"",
  IS_LOCK: false,
  LLOG_IN: "",
  IS_ONLINE: "",
  LLOG_OUT: "",
})
const botton = ref<boolean>(true);
const disable = ref<boolean>(false);
const listView = ref<list[]>([]);

const viewdata = () =>{
  dto.FILTER;
  $api.get("http://localhost:5094/api/user/viewdata", {FILTER: dto.FILTER}, (data) => {
    listView.value = data; 
    $view.value = "VIEW";
}, (err) => {
  $msg.error(err);
});
};
const viewdetail = (x:list) => {
  dto.US_ID = x.US_ID;
 botton.value = true;
 disable.value = true;
  $api.get("http://localhost:5094/api/user/viewdetail", {US_ID: x.US_ID}, (data) => {
  if (data && data.length > 0) {
    dto.US_ID = data[0].US_ID;
    dto.USER_NAME = data[0].USER_NAME;
    $view.value = "CREATE";
  } else {
    $msg.error("No notes found for this user.");
  }
}, (err) => {
  $msg.error(err);
});
}
const addnew = () => {
  $view.value = "CREATE";
   botton.value = false;
  console.log(dto);
}
const Cancel = () => {
  $view.value = "VIEW";
  dto.US_ID = -1;
  dto.USER_NAME = "";
  dto.PASSWORD ="";
}
const Edit = () =>{
  botton.value = false;
  disable.value = false;
}
const savedata = () => {
 $msg.confirm('Do you want to save?', () => {
  $api.get("http://localhost:5094/api/user/save", dto, (data) =>{
    $msg.success("Save Success", () => {
       viewdata();
        $view.value = "VIEW";
      });
  }, (err) =>{
    $msg.error(err);
  })
 });
}

onMounted (() => {
  viewdata();
})
</script>
