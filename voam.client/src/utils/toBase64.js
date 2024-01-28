export const toBase64 = (file) =>
    new Promise((resolve, rejecet) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => resolve(reader.result);
        reader.onerror = (error) => rejecet(error);
    });