type Callback = () => void;

function createModal(html: string): HTMLElement {
  const modal = document.createElement('div');
  modal.innerHTML = html;
  document.body.appendChild(modal);
  return modal;
}

function removeModal(modal: HTMLElement) {
  modal.classList.add('opacity-0');
  setTimeout(() => modal.remove(), 200);
}

function renderBasicModal(message: string, type: 'success' | 'error', callback?: Callback) {
  const color = type === 'success' ? 'green' : 'red';

  const html = `
    <div class="fixed inset-0 bg-black/50 flex items-center justify-center z-50 transition-opacity duration-200">
      <div class="bg-white rounded-lg shadow-lg p-6 w-full max-w-sm text-center">
        <div class="text-${color}-600 font-bold text-lg mb-4 capitalize">${type}</div>
        <div class="text-gray-800 mb-6">${message}</div>
        <button class="px-4 py-2 bg-blue-700 text-white rounded hover:bg-blue-700 transition" id="closeBtn">
          Close
        </button>
      </div>
    </div>
  `;

  const modal = createModal(html);
  const closeBtn = modal.querySelector('#closeBtn');

  if (closeBtn) {
    closeBtn.addEventListener('click', () => {
      removeModal(modal);
      if (callback) {
        callback(); 
      }
    });
  } else {
    console.warn("Close button not found in modal.");
  }
}
function renderConfirmModal(message: string, onConfirm: Callback) {
  const html = `
    <div class="fixed inset-0 bg-black/50 flex items-center justify-center z-50 transition-opacity duration-200">
      <div class="bg-white rounded-lg shadow-lg p-6 w-full max-w-sm text-center">
        <div class="text-blue-600 font-bold text-lg mb-4">Confirm</div>
        <div class="text-gray-800 mb-6">${message}</div>
        <div class="flex justify-center gap-4">
          <button class="px-4 py-2 bg-gray-300 text-gray-800 rounded hover:bg-gray-400" id="cancelBtn">
            No
          </button>
          <button class="px-4 py-2 bg-blue-600 text-white rounded hover:bg-blue-700" id="confirmBtn">
            Yes
          </button>
        </div>
      </div>
    </div>
  `;

  const modal = createModal(html);
  modal.querySelector('#cancelBtn')?.addEventListener('click', () => removeModal(modal));
  modal.querySelector('#confirmBtn')?.addEventListener('click', () => {
    removeModal(modal);
    onConfirm();
  });
}

const $msg = {
  success: (message: string, callback?: Callback) => renderBasicModal(message, 'success', callback),
  error: (message: string, callback?: Callback) => renderBasicModal(message, 'error', callback),
  confirm: (message: string, onConfirm: Callback) => renderConfirmModal(message, onConfirm),
};

export default $msg;
