<template>
  <Header/>
  <div v-show="$view =='VIEW'" class="w-full h-screen bg-gray-50 p-6">
    <div class="flex items-center justify-between mb-4">
      <h1 class="text-2xl font-semibold text-gray-800">Noted</h1>
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

    <div  class="overflow-auto h-[calc(100vh-180px)]" >
      <table class="min-w-full text-sm border-separate border-spacing-y-2">
        <thead class="bg-gray-100 text-gray-700 font-semibold text-left sticky top-0 z-10">
          <tr>
            <th class="px-4 py-2">No</th>
            <th class="px-4 py-2">Title</th>
            <th class="px-4 py-2">Content</th>
            <th class="px-4 py-2">Create Date</th>
            <th class="px-4 py-2">Action</th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="(x, i) in listView"
            :key="x.NT_ID"
            class="hover:bg-gray-50"
          >
            <td class="px-4 py-2">{{ i + 1 }}</td>
            <td class="px-4 py-2 capitalize">{{ x.TITLE }}</td>
            <td class="px-4 py-2">{{ x.CONTENT }}</td>
           <td class="px-4 py-2">
              {{
                new Date(x.CREATE_AT).toLocaleDateString('en-GB', {
                  day: '2-digit',
                  month: 'short',
                  year: 'numeric'
                }).replace(/ /g, '-')
              }}
            </td>
            <td class="px-4 py-2 text-lg flex space-x-3">
            <EyeIcon
              class="w-5 h-5 text-blue-600 hover:text-blue-800 cursor-pointer"
              @click="viewdetail(x)"
              title="View"
            />
            <TrashIcon
              class="w-5 h-5 text-red-600 hover:text-red-800 cursor-pointer"
              @click="deletedata(x)"
              title="Delete"
            />
          </td>
          </tr>
        </tbody>
      </table>
    </div>
    
  </div>
  <div v-show="$view =='CREATE'" class="max-w p-12 bg-white">
   

    <div class="mb-4">
      <label class="block text-sm font-medium text-gray-700 mb-1">Title</label>
      <input
      
        type="text"
        v-model="dto.TITLE"
        placeholder="Enter title"
        :disabled="disable"
        class="w-full px-4 py-2 border border-gray-300 rounded-md shadow-sm focus:ring-blue-500 focus:border-blue-500"
      />
    </div>

    <div class="mb-4">
      <label class="block text-sm font-medium text-gray-700 mb-1">Content</label>
      <textarea
        v-model="dto.CONTENT"
        rows="5"
        placeholder="Enter content"
         :disabled="disable"
        class="w-full px-4 py-2 border border-gray-300 rounded-md shadow-sm focus:ring-blue-500 focus:border-blue-500"
      ></textarea>
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
    Save Note
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
import { EyeIcon, TrashIcon,MagnifyingGlassIcon  } from '@heroicons/vue/24/outline'
import useSessionStore from '../stores/Session';
const $session = useSessionStore();
const $view = ref("VIEW");

interface list {
  NT_ID: number,
  NT_CODE: string,
  TITLE: string,
  CONTENT: string,
  CREATE_AT:Date,
  UPDATE_AT: Date,
}
interface Dtos {
  NT_ID: number,
  TITLE: string,
  CONTENT: string,
  FILTER: string,
  USER_NAME: string,
}
const dto = reactive<Dtos>({
  NT_ID:-1,
  TITLE: "",
  CONTENT:"",
  FILTER:"",
  USER_NAME: $session.USER_NAME,
})
const botton = ref<boolean>(true);
const disable = ref<boolean>(false);
const listView = ref<list[]>([]);

const viewdata = () =>{
  dto.FILTER;
  $api.get("http://localhost:5094/api/noted/viewdata", dto, (data) => {
    listView.value = data; 
    $view.value = "VIEW";
}, (err) => {
  $msg.error(err);
});
};
const viewdetail = (x:list) => {
  dto.NT_ID = x.NT_ID;
 botton.value = true;
 disable.value = true;
  $api.get("http://localhost:5094/api/noted/viewdetail", dto, (data) =>{
    dto.NT_ID = data[0].NT_ID;
    dto.TITLE = data[0].TITLE;
    dto.CONTENT = data[0].CONTENT;
     $view.value = "CREATE";
  }, (err) =>{
    $msg.error(err);
  })
}
const addnew = () => {
  $view.value = "CREATE";
   botton.value = false;
}
const Cancel = () => {
  $view.value = "VIEW";
  dto.NT_ID = -1;
  dto.TITLE = "";
  dto.CONTENT ="";
}
const Edit = () =>{
  botton.value = false;
  disable.value = false;
}
const verifyData =() => {
  if(dto.TITLE == ""){
    $msg.error("Invalid Title!");
    return false;
  }
  return true;
}
const savedata = () => {
  if(!verifyData()) return;
 $msg.confirm('Do you want to save?', () => {
  $api.get("http://localhost:5094/api/noted/save", dto, (data) =>{
    $msg.success("Save Success", () => {
       viewdata();
        $view.value = "VIEW";
      });
  }, (err) =>{
    $msg.error(err);
  })
 });
}
const deletedata =(x:list) =>{
  dto.NT_ID = x.NT_ID;
  $msg.confirm('Are you sure to delete data?', () =>{
    $api.get("http://localhost:5094/api/noted/delete", dto, (data) =>{
    $msg.success("Delete Success",() =>{
      viewdata();
    })
  }, (err) =>{
    $msg.error(err);
  })
  })
}
onMounted (() => {
  viewdata();
})
</script>
