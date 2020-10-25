import { toast } from 'react-toastify';

export default {
    success: (message) => toast.success(message),
    warning: (message) => toast.warn(message),
    error: (message) => toast.error(message),
}