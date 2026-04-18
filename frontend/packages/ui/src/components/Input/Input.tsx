import React from "react";

export type InputProps = React.InputHTMLAttributes<HTMLInputElement> & {
  fullWidth?: boolean;
};

export const Input = React.forwardRef<HTMLInputElement, InputProps>(
  ({ className = "", fullWidth, ...props }, ref) => {
    return (
      <input
        ref={ref}
        {...props}
        className={`
          rounded-md border border-gray-300
          px-3 py-2
          text-sm
          outline-none
          focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500
          transition
          ${fullWidth ? "w-full" : ""}
          ${className}
        `}
      />
    );
  }
);

Input.displayName = "Input";