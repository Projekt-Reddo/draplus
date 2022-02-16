import { useState } from "react";
/**
 * Modal Hook
 * @returns {isShowing, toggle} showing vairable and toggle function
 */
const useModal = () => {
    const [isShowing, setIsShowing] = useState(false);

    function toggle() {
        setIsShowing(!isShowing);
    }
    return {
        isShowing,
        toggle,
    };
};

export default useModal;
