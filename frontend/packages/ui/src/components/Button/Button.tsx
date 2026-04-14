import React from "react";

type Variant = "primary" | "secondary" | "ghost" | "danger";
type Size = "sm" | "md" | "lg";

type ButtonProps = {
  children: React.ReactNode;
  variant?: Variant;
  size?: Size;
  loading?: boolean;
  fullWidth?: boolean;
} & React.ButtonHTMLAttributes<HTMLButtonElement>;

export const Button = React.forwardRef<HTMLButtonElement, ButtonProps>(
  (
    {
      children,
      variant = "primary",
      size = "md",
      loading,
      fullWidth,
      ...props
    },
    ref,
  ) => {
    return (
      <button
        ref={ref}
        {...props}
        disabled={loading || props.disabled}
        className={`
          rounded-lg font-medium transition-all cursor-pointer
          ${fullWidth ? "w-full" : ""}
          ${size === "sm" && "px-3 py-1 text-sm"}
          ${size === "md" && "px-4 py-2"}
          ${size === "lg" && "px-6 py-3 text-lg"}
          ${
            variant === "primary" &&
            "bg-indigo-500 text-white hover:opacity-90 hover:bg-indigo-600"
          }
          ${variant === "secondary" && "bg-white text-gray-900 hover:bg-gray-100 border-gray-200"}
          ${variant === "ghost" && "bg-transparent hover:bg-gray-100 text-gray-600"}
          ${variant === "danger" && "bg-red-500 text-white hover:bg-red-600"}
          ${loading && "opacity-50 cursor-not-allowed"}
        `}
      >
        {loading ? "Loading..." : children}
      </button>
    );
  },
);
