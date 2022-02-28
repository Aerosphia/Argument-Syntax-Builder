/*
    This is the JavaScript version of the library. There is also a C# verison.
    This code is really messy. Just messing around for now. Library isn't finished.
*/

class ArgumentSyntaxBuilder {
    #build = "";
    #append(input) {
        if (!this.#build) {
            this.#build = input;
        } else {
            this.#build = `${this.#build} ${input}`;
        }
    }

    makeRegular(input, options) {
        if (typeof input !== "string") {
            throw new Error("SyntaxBuilder::makeRegular: Not a string");
        }

        if (!options?.optional) {
            this.#append(`<${input}>`);
        } else {
            this.#append(`<?${input}>`);
        }

        return this;
    }

    makeChoice(inputs, options) {
        if (!Array.isArray(inputs)) {
            throw new Error("SyntaxBuilder::makeChoice: Not an array");
        }

        const def = options?.default;
        if (def && inputs.indexOf(def) === -1) {
            throw new Error("SyntaxBuilder::makeChoice: Default input not found");
        }

        if (options?.exactify) {
            inputs = inputs.map((el) => `"${el}"`);
        }

        const splitString = inputs.join(" | ");

        if (!options?.optional) {
            this.#append(`<${splitString}${def ? ` def = "${def}"` : ""}>`);
        } else {
            this.#append(`<?${splitString}${def ? ` def = "${def}"` : ""}>`);
        }

        return this;
    }

    endBuild() {
        if (!this.#build) {
            throw new Error("SyntaxBuilder::endBuild: No build started");
        }

        const oldBuild = this.#build;
        this.#build = "";
        return oldBuild;
    }
}

const SyntaxBuilder = new ArgumentSyntaxBuilder();

const Syntax = SyntaxBuilder.makeRegular("SomeText")
    .makeRegular("SomeMoreText", { optional: true })
    .makeChoice(["Alpha", "Delta", "Jersey"], { optional: true, exactify: true, default: "Alpha" })
    .endBuild();

console.log(Syntax);
// <SomeText> <?SomeMoreText> <?"Alpha" | "Delta" | "Jersey" def = "Alpha">
